
select group_id, left(descr, len(descr)-1) descr from
(
	select group_id, (select descr + ',' as 'data()' from my_table t2 where t1.group_id = t2.group_id for xml path('')) descr
	from my_table t1
	group by group_id
) t;
