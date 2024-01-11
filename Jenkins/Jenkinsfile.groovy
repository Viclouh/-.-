pipeline {
    agent any
  
    environment {
        registry = ""
        dockerContainerName_API = 'AKVT.Raspisanie-API-Dev'
        dockerContainerName_WEB = 'AKVT.Raspisanie-WEB-Dev'
        dockerImageName_API = 'yomaya/akvt.raspisanie.api:dev'
        dockerImageName_WEB = 'yomaya/akvt.raspisanie.web:dev'
        PROJECT_API = './API/'
        PROJECT_WEB = './Web/'
    }
     parameters {
        string(name: "Enviroment", defaultValue: "Development", trim: true, description: "Введите окружение проекта")
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
                    sh 'docker compose -f ./DockerCompose.${Enviroment}.yml up -d'
                    
                }
            }
        }
    }
}