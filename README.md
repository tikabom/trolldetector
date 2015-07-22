# trolldetector
Running the web service on Mac OSX
1. Clone trolldetector from https://github.com/tikabom/trolldetector
2. Execute TrollDetector.exe @ trolldetector/TrollDetector/bin/Debug
3. If step 2 fails download Mono from http://www.mono-project.com/docs/getting-started/install/mac/
4. In your console run this command: mono "Path to TrollDetector.exe"

Testing the web service
1. To obtain a sample text and list of words to excluded, run the service and make a GET request to the URL localhost:8000/TrollDetector/ 
2. To test word count, run the service and make a POST request to the URL localhost:8000/TrollDetector/ with a JSON object in the Request body that looks like:
  {
    Text: "some text",
    Exclude: ["some"],
    WordCount: [{"text", 1}]
  }
