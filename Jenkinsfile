properties([pipelineTriggers([githubPush()])])

pipeline {
	   agent any

	   options { 
	   	disableConcurrentBuilds() 
	   	buildDiscarder(logRotator(numToKeepStr: '10'))
	   }	   

	   environment {           
          path_to_appsetting = 'WebApplication1'
          service_GitURL = 'https://github.com/TrainingAwsBasicEngineer/TrainingPrivate.git'
    	}

	   stages {


	   	  
		   	  // stage('Checkout'){

		   	  // 	steps {
			   	 //  	checkout([
			     //                            $class: 'GitSCM', 
			     //                            branches: [[name: "*/${env.GIT_BRANCH}"]], 
			     //                            doGenerateSubmoduleConfigurations: false, 
			     //                            extensions: [[$class: 'CleanCheckout']], 
			     //                            submoduleCfg: [], 
			     //                            userRemoteConfigs: [[credentialsId: '79d29408-11a6-48cf-a8d3-cdcbca9a0b01', 
			     //                                                 url: "${service_GitURL}"]]
			     //                        ])
		   	  // 	}
	   	  	// 	}


			
		  stage('Prod Protection') {
		  	when {
                branch '*PROD*'                
            }
            options {
			      timeout(time: 100, unit: 'SECONDS') 
			  }
	         steps {
	         	script {
	         			awsProfile = "GFProd"				         	

		         		def userInput = input(
		                 id: 'userInput', message: 'Enter prod password', 
		                 parameters:[ 
		                 [$class: 'TextParameterDefinition', defaultValue: 'None', description: 'Input your password', name: 'Password']		                 
		                ])
		                echo ("Password: "+userInput['Password'])		


		                if(userInput['Password'] != "1234"){
		                	error "stop"
		                }
		                	                			               
		            }		              
	            }

	         	
         }


	       	       	      
	      
	      stage('Build') {
	         steps {	  
	         	sh "echo ${env}"          
	            sh "ls -al"
	            sh "dotnet build"	            
	            // sh "dotnet publish"	            
	         }
	      }  

	      stage('Test'){
	      	steps{
	      		sh "dotnet test"
	      	}
	      }

	      stage('Deploy'){
	      	steps{
	      		sh """
		      			export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
						dotnet lambda deploy-function
		      		"""
	      	}
	      }
	     

	   }


	   post{
	          always{
	              deleteDir()
	              echo 'Clear Directory OK'
	          }
	      }
	}
