echo -e "\n\n[1/4] ********************* Pull latest changes *********************\n\n"
git pull
echo -e "\n\n[2/4] ********************* Kill running app ************************\n\n"
pkill -f "./HeroicBrawlServer/bin/Release/net6.0/HeroicBrawlServer"
echo OK.
echo -e "\n\n[3/4] ********************* Build release ***************************\n\n"
dotnet publish -c Release
echo -e "\n\n[4/4] ********************* Launch app ******************************\n\n"
nohup ./HeroicBrawlServer/bin/Release/net6.0/HeroicBrawlServer &
