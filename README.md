# Company Employees Test Task
This is a solution to the test task, see the description of this task in the file TASK.md.

In this solution we consider that all the employees have unique names for the simplicity.

In this solution we also calculate salary for whole one month for an employee independently on a day of month of a calculation date and independetaly on a day of month where the employee has got started work for the company. So for example, if an emploee has been gotten onboard 31 Jan and we calculate salary for him/her with calculation date 01 Jan thas we calculate salary for whole Jan for this emploee.

## 1. Description of the solution.
### 1.1. Interfaces.
There are 4 interfaces which present common essences of this solution.

IEmployee interface represents an employee API. It provides common employee's properties and a method CalculateSalary to calculate employee's salary.

IManager interface extands IEmployee API for employees who have suborinates. IManager provides property Employees which contains all the directly subordinates (The first level of subordinates). In addition, IManger provides some methods manage of the subordinates. There are 2 methods to add and remove a subordinate and 2 methods GetFirstLevelSubordinatesSalarySumm and GetAllLevelsSubordinatesSalarySumm to calculate sum of subordinates' salaries.

ICompany interface provides public API for a company. It contains Employees property which provides all the employees of the company as a flat IEnumerable. Also ICompany provides some methods to calculate salary of a certain employee, sum of salaries of all the employess and to check if an emploee did work for gthe company on a certain date.

IEmployeesProvider interface just provides a method to get the employees for the company as a hierarchical tree.

### 1.2. Implementation.
There are following classes which implements all the types of employees:

Employee class implements IEmployee interface and all the general logic of all the employees independently on there types. Also this class represents the employee type 'employee'.

AbstractManager class extands Employee class and provides an abstract implementation of the IManger interface. This class implements general logic of all the employees who have sabordinates. GetFirstLevelSubordinatesSalarySumm method of this class just goes through all the direct subordinates, calculates there salaries and summ these salaries. GetAllLevelsSubordinatesSalarySumm methos of this class to perform calculation just goes through all the subordinates level by level.

Manager class extands AbstractManager class and it represents the employee type 'manager'. Manager class implements specific for the employee type 'manager' logic.

Sales class extands AbstractManager class and it represents the employee type 'sales'. Sales class implements specific for the employee type 'sales' logic.

Also there are classes Company which implements ICompany interface and EmployeesProvider whic implements IEmployeesProvider interface.

Company class contains a dictionary of all the employees to have ability calculate employee's salary by employee's name. The class uses Manager class as root entity for the employees hierarchical tree and Comapny class just calls method GetAllLevelsSubordinatesSalarySumm of this root entity to calculate sum of all the employees' slaray.

EmployeesProvider class just provides a hardcoded herarchical employees tree for the company.

### 1.3. Unit Tests.
This solution contains unit tests for general business logic. Tests for Employee, Manager, Sales and Company logic exist. CompanyTests shows using mock objects in testing.

## 2. Pros, cons and improvements.
### 2.1. Pros.
It is a simple and clear architecture. Business logic implementation is quite simple which allows to avoid of spending lots of time on its development. This implementation is enough for some prototype or demonstartor or even for a tiny company where there is no a lot of amount of employees.

### 2.2. Cons and improvements.
#### Disadvantage 1:
So as salary of managers and sales depends on salary of other employees and sale's salary directly depents on salary of employees on all the underlayed levels of the salary tree branch we have to calculate salaries for the same employees again and again many, many times. This circamstance has a huge negative impact on calculation performance with a quite deep and whide employees tree.
#### Improvment 1:
Implement cach unit to keep calculation result on a date and use it in Employee class for salary and  in AbstractEmployee class for summs of salaries. Also we need implemet here a mechanism of cach invalidation. When some employee's data such as base salary or onboarding date is changed or adding/removing a subobordinate we have to spread a cach invalidation event from changed employee up through his/her employees tree branch to the root node of this tree.
This improvment allows us to avoid multipl reaclculate salary for the same employee.
#### Disadvantage 2:
So as when we need to calculate sum of salaries for all the underneath employee levels we perform it from top level to low level the calculation algorithm goes down and up through a branch many times even with cache implemntation. It reduses calculation performanse.
#### Improvement 2:
Start calculation of salaries summ for all the levels from lowest level and go up to the top level in a branch of employeers tree. This approach with implemented cach allows us to get rid off going up and down many times.
#### Disadvantage 3:
All the calculations perform one by one although calculations for different branches dont depend on each other. This approach does not allow us to use all awalable calculation power.
#### Improvement 3:
Use queue for calculation tasks and several threads to peform these tasks from the queue. Do not use async/await here to awoid a 'threads starvation' problems so as its calculation tasks.
#### Disadvantage 4:
What if we need to calculate salaries for several different date in the same time? The solution doe not allow us to do it at all.
#### Improvement 4:
Implement thread safity in this solution. Expand the cach implementation to keep data for several dates and  improve managment of this cache.
#### Disadvantage 5:
Current approach does not allow to change calculation rule of salary quite flexible and use personal rules for employees.
#### Improvement 5:
Create different entity of variable payment  or bonus, for example IBonus. Different implementations of this interface will provide various rules to calculate variable payments. Each employee should contain just a list of these IBonus objects. In this case emploees' types will not depend on salary calculation rules. 

There are many other improvements which could be implemented on a way from a simple test task to a commercial product even if this product quite simple.
