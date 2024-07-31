# Company Employees Test Task

# Task:

There is a company which can have employees. An employee has the following properties: name, date of employment, base salary rate (for the simplisity each employee type has the same base salary rate value by default).
There are 3 employee types - Employee, Manager, Sales. Each employee can have a boss. Each employee except type Employee can has subordinates.
A salary of an employee of type Employee is a base rate plus 3% for each year of working in the company, but total extra payment is not more 30% of the base salary.
A salary of an employee of type Manager is a base rate plus 5% for each year of working in the company (but total this extra payment is not more 40% of the base salary) plus 0,5% of summary salary of all the first level subordinates.
A salary of an employee of type Sales is a base rate plus 1% for each ear of working in the company (but total this extra payment is not more 35% of the base salary) plus 0,3% of summary salary of all the all the levels subordinates.
An employee (except type Employee) can has any amount of subordinates of any types.

It is required to create an architecture of classes which describes this model and also to implement a salary calculation algorithm for each employee on any point of time (and also calculating a summary salary of all the employees of that company) with C# (web service/console/user interface is up to you, it is not significant for this task).

The system should be tested by unit-tests (nUnit) /it is not mandatory to have a full coverage, but there should be demonstrative tests to test a business logic of that system/.

Besides it is required to write (on English) a brifly review for your self solution to the test task. Describe pros and cons of the architecture (what can be improved or changed or some else thoughts to use this solution in real tasks).
Comments in the source code should be written on English.

# Задатние:

Есть компания, у компании могут быть сотрудники. Сотрудник характеризуется именем, датой поступления на работу, базовой ставкой (для простоты, это значение по-умолчанию одинаково для всех видов сотрудников).
Сотрудники бывают 3 видов - Employee, Manager, Sales. У каждого сотрудника может быть начальник. У каждого сотрудника    кроме Employee могут быть подчинённые.
Зарплата сотрудника Employee - это базовая ставка плюс 3% за каждый год работы в компании, но не больше 30% суммарной надбавки
Зарплата сотрудника Manager - это базовая ставка плюс 5% за каждый год работы в компании (но не больше 40% суммарной надбавки) плюс 0,5% зарплаты всех подчинённых первого уровня
Зарплата сотрудника Sales - это базовая ставка плюс 1% за каждый год работы в компании (но не больше 35% суммарной надбавки) плюс 0,3% зарплаты всех подчинённых  всех уровней
У сотрудников (кроме Employee) может быть любое количество подчинённых любого вида.

Требуется: составить архитектуру классов, описывающих данную модель, а также реализовать алгоритм расчета зарплаты каждого сотрудника на произвольный момент времени (а также подсчёт суммарной зарплаты всех сотрудников фирмы в целом) с помощью c# (веб сервис/консоль/пользовательский интерфейс на выбор, это не существенно для данной задачи).

Система должна быть проверена unit-testами (nUnit) /не обязательно полное покрытие, но должны быть показательные тесты для проверки бизнес-логики/.

Кроме того, требуется написать (на английском) краткий обзор своего решения тестовой задачи, описав плюсы и минусы архитектуры (что можно улучшить или поменять или еще какие-то соображения для использования решения в реальных целях).
Комментарии в исходном требуется написать на английском языке
