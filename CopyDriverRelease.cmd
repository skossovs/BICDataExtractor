copy BIC.AllTests\BIC.ScrapperTests\chromedriver.zip BIC.AllTests\BIC.ScrapperTests\bin\Release\
copy BIC.Apps\BIC.Apps.MSMQExtractorCommander\chromedriver.zip BIC.Apps\BIC.Apps.MSMQExtractorCommander\bin\Release\
copy BIC.Apps\BIC.Apps.ExtractorCommander\chromedriver.zip BIC.Apps\BIC.Apps.ExtractorCommander\bin\Release\
copy BIC.Scrappers\BIC.Scrappers.Utils\chromedriver.zip BIC.Scrappers\BIC.Scrappers.Utils\bin\Release\

set mypath=%cd%

cd %mypath%\BIC.AllTests\BIC.ScrapperTests\bin\Release\
tar -xf chromedriver.zip
del chromedriver.zip

cd %mypath%\BIC.Apps\BIC.Apps.MSMQExtractorCommander\bin\Release\
tar -xf chromedriver.zip
del chromedriver.zip

cd %mypath%\BIC.Scrappers\BIC.Scrappers.Utils\bin\Release\
tar -xf chromedriver.zip
del chromedriver.zip

cd %mypath%\BIC.Apps\BIC.Apps.ExtractorCommander\bin\Release\
echo %cd%
tar -xf chromedriver.zip
del chromedriver.zip


pause