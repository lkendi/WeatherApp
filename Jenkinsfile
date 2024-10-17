pipeline{
    agent any
    stages{
        stage('Checkout'){
            steps{
                echo 'Checking out the repository...'
                checkout scmGit(branches: [[name: '*/main']], extensions: [], userRemoteConfigs: [[url: 'https://github.com/lkendi/WeatherApp.git']])
            }
        }
        
        stage('.NET Build'){
            steps{
                echo 'Building project...'
                dir('src') { 
                    sh 'dotnet restore'
                    sh 'dotnet build'
                }
            }
        }
        
        stage('Build Docker Image') {
            steps {
                echo 'Building docker image...'
                script {
                    dir('src') {
                        sh 'docker build -t lkendi/weatherapp:latest .'
                    }
                }
            }
        }
        
        stage('Push Image to Docker Hub'){
            steps{
                echo 'Pushing image to docker hub...'
                script{
                    withCredentials([string(credentialsId: 'dockerhub-id', variable: 'dockerhubcreds')]) {
                        sh 'docker login -u lkendi -p ${dockerhubcreds}'
                    }
                    sh 'docker push lkendi/weatherapp'
                }
            }
        }
    }
}
