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
Assumption: The service expects the client to send its request in JSON containing three properties: Text (text used for the word count), Exclude (list of words that should not be counted), WordCount (every word that is counted and its corresponding count).

To return sample text and list of words to be excluded:
The service has access to files that contain text. When a client requests for some text from the service to perform word count on, the service randomly picks a file. The text in the file is parsed to extract a list of unique words. If the text contains only one unique word, the text is returned to the client with an empty list for the exclusion words. If there are more unique words, the number of words N to be added to the exclusion list is chosen at random based on the number of unique words. 
