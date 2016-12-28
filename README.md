# Oql OData Server Alpha Prototype

<img border="0" src="https://github.com/vadimprogsource/Oql.OData/blob/master/infrastructure.jpg">


Instruction:

1) Build Oql.OData solution and web deploy on your IIS<br/>
2) Rewrite your appsettings.json (data string connection for your Ms Sql Server) <br/>
3) Run on your inet browser http://server_name/odata/any_table_name?$top=10 

For example: 
http://server/odata/customer?$top=10$orderby=id,name desc$filter=id ge 1$select=id,name

 
