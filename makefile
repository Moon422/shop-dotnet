proj=Shop.Net
data_proj=$(proj).Data
api_proj=$(proj).API
mvc_proj=$(proj).Web
cc=dotnet
app=app

run:
	$(cc) run --project $(api_proj) 

watch:
	$(cc) watch run --project $(api_proj)

migrate:
	$(cc) ef migrations add "$(name)" --project $(api_proj) 
	$(cc) ef database update --project $(api_proj) 
	git add $(proj)/Migrations 
	git commit -m "$(name)" 

publish:
	$(cc) publish -c Release -r linux-x64 --self-contained -o /var/www/$(app) 