copy BIC.AllTests\BIC.ScrapperTests\chromedriver.zip BIC.AllTests\BIC.ScrapperTests\bin\Debug\
copy BIC.Apps\BIC.Apps.MSMQExtractorCommander\chromedriver.zip BIC.Apps\BIC.Apps.MSMQExtractorCommander\bin\Debug
copy BIC.Apps\BIC.Apps.ExtractorCommander\chromedriver.zip BIC.Apps\BIC.Apps.ExtractorCommander\bin\Debug
copy BIC.Scrappers\BIC.Scrappers.Utils\chromedriver.zip BIC.Scrappers\BIC.Scrappers.Utils\bin\Debug

set mypath=%cd%

cd %mypath%\BIC.AllTests\BIC.ScrapperTests\bin\Debug\
tar -xf chromedriver.zip
del chromedriver.zip

cd %mypath%\BIC.Apps\BIC.Apps.MSMQExtractorCommander\bin\Debug\
tar -xf chromedriver.zip
del chromedriver.zip

cd %mypath%\BIC.Scrappers\BIC.Scrappers.Utils\bin\Debug\
tar -xf chromedriver.zip
del chromedriver.zip

cd %mypath%\BIC.Apps\BIC.Apps.ExtractorCommander\bin\Debug\
tar -xf chromedriver.zip
del chromedriver.zip


pause