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
	$(cc) ef migrations add "$(name)" --project $(data_proj) 
	$(cc) ef database update --project $(data_proj) 
	git add $(data_proj)/Migrations 
	git commit -m "$(name)" 

publish:
	$(cc) publish -c Release -r linux-x64 --self-contained -o /var/www/$(app) 