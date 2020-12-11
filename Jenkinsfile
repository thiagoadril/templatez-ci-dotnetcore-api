def DOCKER_TOOL_NAME
def BASE_FOLDER_NAME
def API_PROJECT_NAME
def API_IMAGE_NAME
def API_NETWORK_NAME
def API_FOLDER_NAME
def API_FILE_NAME_SLN
def API_TEST_FOLDER_NAME
def API_TEST_FILE_NAME_CSPROJ
def API_TEST_FILE_REPORT_NAME

pipeline {
    agent any

    stages {
	
		/*
		 *	[INIT]
		 */
		stage('INIT') {
            steps{
                script{
					DOCKER_TOOL_NAME="Default"
					BASE_FOLDER_NAME="app"
					API_PROJECT_NAME="templatez_api"
					API_IMAGE_NAME="company_templatez_api"
					API_NETWORK_NAME="company_templatez_network_api"
                    API_FOLDER_NAME="Templatez.Backend/Templatez.Api"
					API_FILE_NAME_SLN="Templatez.Backend/Templatez.Backend.sln"
                    API_TEST_FOLDER_NAME="Templatez.Backend/Templatez.UnitTests"
                    API_TEST_FILE_NAME_CSPROJ="Templatez.UnitTests.csproj"
					API_TEST_FILE_REPORT_NAME="app.tests"
                }
            }                
        }

		/*
		 *	[CLEAN - (START)]
		 */
		stage('CLEAN - (START)') {
			steps {
				echo "-----------------------------------"
				echo 'Initial cleaning running....'
				script {
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/build")])
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/image")])
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci")])
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_TEST_FOLDER_NAME}/ci/build")])
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_TEST_FOLDER_NAME}/ci")])
				}
				echo '-----------------------------------'
			}
		}
		
		/*
		 *	[BUILD]
		 */
        stage('BUILD') {
            steps {
				echo '-----------------------------------'
				echo 'Generating build...'
				script {
					docker.withTool(DOCKER_TOOL_NAME) {
						def image = docker.image('mcr.microsoft.com/dotnet/core/sdk:3.1')
						image.pull()
						image.inside() {
							sh "dotnet publish -c Release -o ${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/build ${BASE_FOLDER_NAME}/${API_FILE_NAME_SLN}"
							sh "dotnet publish -c Release -o ${BASE_FOLDER_NAME}/${API_TEST_FOLDER_NAME}/ci/build ${BASE_FOLDER_NAME}/${API_TEST_FOLDER_NAME}/${API_TEST_FILE_NAME_CSPROJ}"
						}
					}
				}
				echo '-----------------------------------'
            }
        }
		
		/*
		 *	[TESTS]
		 */
		stage('TESTS') {
            steps {
				echo '-----------------------------------'
				echo 'Testing app...'
				script {
					docker.withTool(DOCKER_TOOL_NAME) {
							def image = docker.image('mcr.microsoft.com/dotnet/core/sdk:3.1')
							image.pull()
							image.inside() {
								sh "dotnet test ${BASE_FOLDER_NAME}/${API_TEST_FOLDER_NAME}/${API_TEST_FILE_NAME_CSPROJ} --logger:'trx;LogFileName=${API_TEST_FILE_REPORT_NAME}.${env.BRANCH_NAME}.xml' --results-directory './reports'" 
							}
					}
				}
				echo '-----------------------------------'
            }
        }

		/*
		 *	[IO (FILES)]
		 */
		stage('IO (FILES)') {
		    steps  {
				echo '-----------------------------------'
				echo 'IO starting...'
				script {
					fileOperations([fileDeleteOperation(excludes: '', includes: "${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/build/*.pdb")])
					fileOperations([folderCopyOperation(destinationFolderPath: "${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/image/", sourceFolderPath: "${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/build/")])
					fileOperations([fileCopyOperation(excludes: '', flattenFiles: true, includes: "${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/Dockerfile*", targetLocation: "${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/image")])
				}
				echo '-----------------------------------'
			}
        }
		
		/*
		 *	[IMAGES]
		 */
        stage('IMAGES') {
            steps {
				echo '-----------------------------------'
				echo 'Generating app image...'
				script {
				    sh 'ls'
					if(env.BRANCH_NAME.contains('master')) {
						docker.withTool(DOCKER_TOOL_NAME) {
							def rootimage = docker.image('mcr.microsoft.com/dotnet/core/aspnet:3.1')
							rootimage.pull()
							def image = docker.build("${API_IMAGE_NAME}_${env.BRANCH_NAME.replace('feature/','').replace('release/','').toLowerCase()}:${env.BUILD_ID}","${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/image/")
							image.tag("latest");
						}
					} else {
						docker.withTool(DOCKER_TOOL_NAME) {
							def rootimage = docker.image('mcr.microsoft.com/dotnet/core/aspnet:3.1')
							rootimage.pull()
							def image = docker.build("${API_IMAGE_NAME}_${env.BRANCH_NAME.replace('feature/','').replace('release/','').toLowerCase()}","${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/image/")
							image.tag("latest");
						}
					}
				}
				echo '-----------------------------------'
            }
        }
		
		/*
		 *	[ENVIRONMENT]
		 */
		stage('ENVIRONMENT') {
			steps {
				echo '-----------------------------------'
				echo 'Configure application environment...'
				script {
					docker.withTool(DOCKER_TOOL_NAME) {
						def suffix  = "${env.BRANCH_NAME.replace('feature/','').replace('release/','').toLowerCase()}"
						def image = docker.image('docker/compose:1.23.2')
						image.pull()
						
						withDockerContainer(args: '--entrypoint=\'\'', image: 'docker/compose:1.23.2', toolName: DOCKER_TOOL_NAME) {
							withEnv(["IMAGE_SUFFIX=${suffix}"]) {
								switch(env.BRANCH_NAME) {
								  case "master":
									sh "docker network ls|grep ${API_NETWORK_NAME}_production > /dev/null || docker network create --driver bridge ${API_NETWORK_NAME}_production"
									sh "cp docker/env/docker-env-production.env .env"
									sh "docker-compose -f docker/compose/docker-compose.yaml -f docker/compose/docker-compose-production.yaml --project-name ${API_PROJECT_NAME}_${suffix} up -d"
									break
								  case "staging":
									sh "docker network ls|grep ${API_NETWORK_NAME}_staging > /dev/null || docker network create --driver bridge ${API_NETWORK_NAME}_staging"
									sh "cp docker/env/docker-env-staging.env .env"
									sh "docker-compose -f docker/compose/docker-compose.yaml -f docker/compose/docker-compose-staging.yaml --project-name ${API_PROJECT_NAME}_staging up -d"
									break
								  case "testing":
									sh "docker network ls|grep ${API_NETWORK_NAME}_testing > /dev/null || docker network create --driver bridge ${API_NETWORK_NAME}_testing"
									sh "cp docker/env/docker-env-testing.env .env"
									sh "docker-compose -f docker/compose/docker-compose.yaml -f docker/compose/docker-compose-testing.yaml --project-name ${API_PROJECT_NAME}_testing up -d"
									break
								  case "develop":
									sh "docker network ls|grep ${API_NETWORK_NAME}_development > /dev/null || docker network create --driver bridge ${API_NETWORK_NAME}_development"
									sh "cp docker/env/docker-env-development.env .env"
									sh "docker-compose -f docker/compose/docker-compose.yaml -f docker/compose/docker-compose-development.yaml --project-name ${API_PROJECT_NAME}_development up -d"
									break;
								}
							}
						}
					}
				}
				echo '-----------------------------------'
			}
		}
		
		/*
		 *	[CLEAN (END)]
		 */
		stage('CLEAN (END)') {
			steps {
				echo '-----------------------------------'
				echo 'End cleaning running....'
				script {
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/build")])
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci/image")])
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_FOLDER_NAME}/ci")])
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_TEST_FOLDER_NAME}/ci/build")])
					fileOperations([folderDeleteOperation("${BASE_FOLDER_NAME}/${API_TEST_FOLDER_NAME}/ci")])
				}
				echo '-----------------------------------'
			}
		}		
    }
	
	post {
		always {
			step([$class: 'XUnitPublisher',
				testTimeMargin: '3000',
				thresholdMode: 1,
				thresholds: [
					[$class: 'FailedThreshold', failureNewThreshold: '', failureThreshold: '', unstableNewThreshold: '', unstableThreshold: ''],
					[$class: 'SkippedThreshold', failureNewThreshold: '', failureThreshold: '', unstableNewThreshold: '', unstableThreshold: '']],
				tools: [
					[$class: 'MSTestJunitHudsonTestType', pattern: 'reports/*.xml', deleteOutputFiles: true, failIfNotNew: true, skipNoTestFiles: true, stopProcessingIfError: true]]])
		}
	}
}