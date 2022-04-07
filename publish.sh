echo "1/4 ********************* Pull latest changes *********************"
git pull
echo "2/4 ********************* Kill running app ************************"
pkill -f "./HeroicBrawlServer/bin/Release/net6.0/HeroicBrawlServer"
echo "3/4 ********************* Build release ***************************"
dotnet publish -c Release
echo "4/4 ********************* Launch app ******************************"
nohup ./HeroicBrawlServer/bin/Release/net6.0/HeroicBrawlServer &
