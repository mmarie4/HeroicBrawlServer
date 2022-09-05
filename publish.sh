echo -e "\n\n[1/5] ********************* Pull latest changes *********************\n\n"
git pull
echo -e "\n\n[2/5] ********************* Kill running app ************************\n\n"
pkill -f "./HeroicBrawlServer/bin/Release/net6.0/HeroicBrawlServer"
echo OK.
echo -e "\n\n[3/5] ********************* Build release ***************************\n\n"
dotnet publish -c Release
echo -e "\n\n[4/5] ********************* Backup logs *****************************\n\n"
now=$(date +"%Y-%m-%d")
mv nohup.out ./logs/logs-$now.txt
echo -e "\n\n[5/5] ********************* Launch app ******************************\n\n"
nohup ./HeroicBrawlServer/bin/Release/net6.0/HeroicBrawlServer &
echo -e "Publish completed"
