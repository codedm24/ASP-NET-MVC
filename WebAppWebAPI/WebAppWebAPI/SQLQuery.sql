SELECT TOP (1000) [Id]
      ,[Number]
      ,[Title]
      ,[Pages]
  FROM [BooksNew].[dbo].[Chapters]

insert into Chapters(Number,Title,Pages) VALUES(1,'Application Architectures',35)
insert into Chapters(Number,Title,Pages) VALUES(2,'Core C#',42)
insert into Chapters(Number,Title,Pages) VALUES(3,'Objects and Types',30)
insert into Chapters(Number,Title,Pages) VALUES(4,'Inheritance',18)
insert into Chapters(Number,Title,Pages) VALUES(5,'Windows Store Apps',45)
insert into Chapters(Number,Title,Pages) VALUES(6,'Windows Store Apps',45)
insert into Chapters(Number,Title,Pages) VALUES(42,'ASP.NET Web API',35)
		 
delete from Chapters where Number=42
