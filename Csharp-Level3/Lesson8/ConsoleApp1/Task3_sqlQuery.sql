
SELECT Name, Count(Name) as Количество FROM Users
	where Name like 'А%'
	group by Name
	having count(Name) > 1;
