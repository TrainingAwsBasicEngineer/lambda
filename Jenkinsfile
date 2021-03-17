pipeline {	   
	   agent { label 'oh-node' } 

	   options { 
	   	disableConcurrentBuilds() 
	   	buildDiscarder(logRotator(numToKeepStr: '10'))
	   }	   

	   environment {           
          path_to_appsetting = 'bankLambdaAPI'
    	}

	   
	   stages {
	      stage('Initialize SourceControl') {
	         steps {  	     
	         		bat('set')	         		
	                bat "echo The current directory is %CD%"
	                bat "dir"	                
	         }
	      }	 

	      stage('Test') {
	         steps {  	
	         	bat "dotnet test"     
	         }
	     }

	      stage('Build') {
	         steps {  	
	         	bat "dotnet publish -o out"     
	         }
	     }

	     stage('Change IIS Path') {
	         steps {  	
	         	bat """ 
	         		IF NOT EXIST "C:\\Data\\Test\\Test-%BUILD_NUMBER%" (
							mkdir "C:\\Data\\Test\\Test-%BUILD_NUMBER%" 
						) 
	         	"""

	         	bat """ 
	         		"C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe" -verb:sync  -source:IisApp="%CD%\\out" -dest:iisapp="C:\\Data\\Test\\Test-%BUILD_NUMBER%"
	         	"""

	         	bat """ 
	         		C:\\Windows\\System32\\inetsrv\\appcmd set vdir "Default Web Site/WebApplication/" -physicalPath:"C:\\Data\\Test\\Test-%BUILD_NUMBER%"
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
