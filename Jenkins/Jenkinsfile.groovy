def getProjEnv(branch){
    if(branch=='main'){
        return 'Production'
    }
    return 'Development'
}
def getDockerVer(branch){
    if(branch=='main'){
        return 'prod'
    }
    return 'dev'
}

pipeline {
    agent any
  
    environment {
        registry = ""
        dockerContainerName_API = 'AKVT.Raspisanie-API-'
        dockerContainerName_WEB = 'AKVT.Raspisanie-WEB-'
        dockerImageName_API = 'yomaya/akvt.raspisanie.api:${getDockerVer(env.BRANCH_NAME)}'
        dockerImageName_WEB = 'yomaya/akvt.raspisanie.web:${getDockerVer(env.BRANCH_NAME)}'
        PROJECT_API = './API/'
        PROJECT_WEB = './Web/'
        projEnvironment = getProjEnv(env.BRANCH_NAME)
    }
    stages {
        
        stage('Build API') {
            steps {
                script {
                    // ���� ��� ������ ������� API
                    dir("${PROJECT_API}") {
                        sh 'docker build -t ${dockerImageName_API} .'
                    }
                }
            }
        }

        stage('Build WEB') {
            steps {
                script {
                    // ���� ��� ������ ������� WEB
                    dir("${PROJECT_WEB}") {
                        sh 'docker build -t ${dockerImageName_WEB} .'
                    }
                }
            }
        }

        stage('Clean and Start Docker Compose') {
            steps {
                script {
                    // ��������� � �������� ����������� �� �� ������
                    sh "docker container stop ${dockerContainerName_API} || true"
                    sh "docker container stop ${dockerContainerName_WEB} || true"
                    sh "docker container rm ${dockerContainerName_API} || true"
                    sh "docker container rm ${dockerContainerName_WEB} || true"

                    // ������ Docker Compose
                    sh 'docker compose -f ./DockerCompose.${projEnvironment}.yml up -d'
                    
                }
            }
        }
    }
}