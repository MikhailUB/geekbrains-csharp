/*
Болотов Михаил
Урок 8. Обзор сервис-ориентированной архитектуры приложений (SOA).
	
Изменить WPF-приложение для ведения списка сотрудников компании (из урока 7), используя веб-сервисы.
Разделите приложение на две части. Первая часть – клиентское приложение, отображающее данные.
Вторая часть – веб-сервис и код, связанный с извлечением данных из БД. Приложение реализует только просмотр данных.
При разработке приложения допустимо использовать любой из рассмотренных типов веб-сервисов.
	1. Создать таблицы Employee и Department в БД MSSQL Server и заполнить списки сущностей начальными данными;
	2. Для списка сотрудников и списка департаментов предусмотреть визуализацию (отображение);
	3. Разработать формы для отображения отдельных элементов списков сотрудников и департаментов.
*/
using System.Web.Http;

namespace EmployeesWebAPIService
{
	public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
