# Build an Object Detection Solution with Microsoft Azure Custom Vision Service

![enter image description here](https://www.pluralsight.com/content/dam/pluralsight/newsroom/brand-assets/logos/pluralsight-logo-vrt-color-2.png)  

Hi!

Here you'll find the demo and study material for my Pluralsight course [Build an Object Detection Solution with Microsoft Azure Custom Vision Service](https://pluralsight.pxf.io/custom-vision-object-detection).

I hope you enjoyed the course. If you have any questions, please don't hesitate don't contact me!

## images-testing

Images used in the course to test the custom vision model. These royalty-free images were downloaded from [Pixabay](https://pixabay.com).

## images-training

Images used in the course to train the custom vision model. These royalty-free images were downloaded from [Pixabay](https://pixabay.com).

## object-detection-app-m1

This .NET project shows you how to work with a custom vision project from code and includes submitting images for object detection and launching a training request.

## Command-line commands used in the Docker container demo

#### Build the Docker image:

docker build "c:\Temp\CustomVisionExport.Linux" --tag fruitdetection

#### List Docker images:

docker image ls

#### Run the Docker image in container:

docker run --detach --publish 127.0.0.1:9001:80 fruitdetection

#### Call the Docker container endpoint with an image from file for object detection:

curl -X POST http://127.0.0.1:9001/image -H "Content-Type: image/jpeg" --data-binary @"c:\Temp\pear-2166102_1280.jpg"

#### Call the Docker container endpoint with an image from a URL for object detection:

curl -X POST http://127.0.0.1:9001/url -d "{ \"url\": \"https://raw.githubusercontent.com/JurgenOnAzure/build-object-detection-solution-microsoft-azure-custom-vision-service/main/images-testing/pear-2166102_1280.jpg\"}"

## References and documentation

#### Landing page for Custom Vision documentation:

https://docs.microsoft.com/en-us/azure/cognitive-services/custom-vision-service/

#### The Custom Vision portal where you manage your projects:

https://www.customvision.ai

#### Custom Vision domains:

https://docs.microsoft.com/en-us/azure/cognitive-services/custom-vision-service/select-domain

#### How to improve your Custom Vision model:

https://docs.microsoft.com/en-us/azure/cognitive-services/custom-vision-service/getting-started-improving-your-classifier

#### Custom Vision code samples:

https://docs.microsoft.com/en-us/samples/browse/?products=azure&term=vision&terms=%22Custom%20Vision%22

#### Tutorial: Perform image classification at the edge with Custom Vision Service:

https://docs.microsoft.com/en-us/azure/iot-edge/tutorial-deploy-custom-vision?view=iotedge-2020-11

#### Pluralsight course 'Microsoft Azure Developer: Deploying and Managing Containers':

https://app.pluralsight.com/library/courses/microsoft-azure-containers-deploying-managing

#### Custom Vision Training API documentation and sandbox:

https://go.microsoft.com/fwlink/?linkid=865446

#### Custom Vision Prediction API documentation and sandbox:

https://go.microsoft.com/fwlink/?linkid=865445

#### Docker Desktop install and documentation:

https://www.docker.com/products/docker-desktop

#### Microsoft documentation and scripts for Docker:

https://github.com/MicrosoftDocs/Virtualization-Documentation/tree/master/windows-server-container-tools

#### Azure Cognitive Services containers:

https://docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-container-support

#### Custom Vision classifier REST API/SDK samples:

https://docs.microsoft.com/en-us/azure/cognitive-services/custom-vision-service/quickstarts/image-classification?tabs=visual-studio&pivots=programming-language-csharp

#### Custom Vision object detection SDK samples:

https://docs.microsoft.com/en-us/azure/cognitive-services/custom-vision-service/quickstarts/object-detection?tabs=visual-studio&pivots=programming-language-csharp

#### Curl command line tool and library for transferring data:

https://curl.se
