proj=Shop.Net
data_proj=$(proj).Data
api_proj=$(proj).API
mvc_proj=$(proj).Web.Admin
cc=dotnet
app=app

run:
	$(cc) run --project $(mvc_proj) 

watch:
	$(cc) watch run --project $(mvc_proj)

migrate:
	$(cc) ef migrations add "$(name)" --project $(data_proj) --startup-project $(data_proj)
	$(cc) ef database update --project $(data_proj) 
	git add $(data_proj)/Migrations 
	git commit -m "$(name)" 
	git push origin  

publish:
	$(cc) publish -c Release -r linux-x64 --self-contained -o /var/www/$(app) 
