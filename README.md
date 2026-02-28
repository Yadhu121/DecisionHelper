# DECISION HELPER

A web based decision making tool made using ASP.NET Core MVC that helps to compare options based on weighted criteria.

## MY UNDERSTANDING OF THE PROBLEM

My understanding of the problem was that I had to make a decision making system which gives the best decision to the user from the options the user listed.
Users should be able to define n number of options, their criteria's which affect their decision, how much a criteria affects their decision and also the values of criteria's of each option.
This system solves the problem of user not being able to take a decision because of their emotion.

## ASSUMPTIONS MADE

### Users can enter the values of every criteria for each option:
Either decimal of Integer and not fixed to a limit to allow flexibility for a real world number such as distance.

### Higher is not always better:
There can be criteria's where it being lower is more ideal, like price.

### Criteria order shows priority:
User should add/order the criteria's in the order of their priority. The most important being first.

### Account is not mandatory:
Guest users can still use this tool, but they wont get personalised recommendations.

## WHY I STRUCTURED THE SOLUTION THIS WAY

The project follows an MVC design pattern for better readability and scalability

### DecisionController:
handles all the decision based things such as fetching recommendations.

### AccountController:
handles all user authentication, login, logout.

### AppDbContext:
is the Entity Framework core of this project which keeps the database. Tables include flair, decision, options, personalised option and criteria

### DecisionService:
Contains the core calculation logic

### Models:
DecisionInput, Option, Criterion, ScoreEntry, ChooseFromTwo

Flair, DecisionRecord, DecisionOptions UserDecisionOption, UserCriterion

Frontend is simple server rendered with HTML, vanilla JS and tailwind CSS.

## DESIGN DECISIONS AND TRADE-OFFS

There are 3 main steps to this

### 1. ROC weight calculation:
criteria's are ranked in the order of priority(In the order the user added them. User can also rearrange after adding.).

Rank order centroid weights are calculated using 

		weight(i) = (1/n) * (1/i + 1/(i+1) + ... + 1/n)

This would remove the need of having the user to give an extra input telling the weight of each criteria.

### 2. Pair wise comparison:
For the top 3 criterion, a best of two comparison is made in which the user answers which is more important to them or are they equally important. This lets the users to fine tune the weight of the criteria's once more according to which is selected in a pair wise comparison.

#### 12% weight transfer:
12% of the weight from the non selected criteria is transferred to the selected one to add more preference to the users more preferred criteria

### 3. Min Max normalisation per criterion:
This brings all the scored to a scale from 0 to 1 regardless of what scale they belong to. This makes sure that 2 criteria with different scale for example, price and taste can be compared fairly.

#### Flair stored as string with check for duplication.
This is simple to implement, allows case insensitive selection thus further preventing duplication.

#### Limiting the pair wise comparison to top3.
This makes the user experience better, but this also means that the weight of the lower ranked criteria's that is the criteria below top3 cannot be adjusted

#### The inputs of criteria's of each option are numeric:
This requires the users to convert their feeling such as taste to a number.

#### There is no interface for admin:
To rename, delete or edit flairs, admin would need to access the DB directly.

## EGDE CASES CONSIDERED

### Equal scores on criteria:
If all the options have the same scores then the normalised value is set to 1 avoiding the division by zero error

### Duplicate flair names:
Controller searches does a case insensitive search to avoid duplicate flairs

### Adding flair:
Adding a flair can bother some people so it is made optional.

### Input validation:
Score input checks if the value is an integer to avoid error in case of non numerical inputs

### Missing option/criteria:
If any option or criteria is having no input user gets a notification/The empty once are not considered.

### Pairwise comparison unanswered:
Then they get marked as answered and no change in weight happens.

### Recommended Option already added:
checks if the recommended option is already added, only adds if not already added

### Multiple options with same winning final score:
The first option which was inputed is shown as the result

## HOW TO RUN THE PROJECT

Prerequisites
.NET8 SDK
PostgreSQL

Steps:
1. Clone the repository

	git clone https://github.com/Yadhu121//DecisionHelper
	cd DecisionHelper

2. Configure the database connection in appsettings.jsomn

	"ConnectionStrings": {
     		"DefaultConnection": "Host=localhost;Database=DecisionHelper;Username=youruser;Password=yourpassword"
   	}
3. Apply database migrations
	
	dotnet ef database update

4. Run
	dotnet run

5. Open the browser and navigate to the localhost:<portnumber>


## WHAT I WOULD IMPROVE WITH MORE TIME

### Flair management:
Create an interface for admin so that they can manage the flairs solving their need to directly access the db

### Frontend:
Switch to framework like react/angular for better scalability

### Frontend:
Change the now scrolling ui to one which displays total number of steps and number of steps left and instead of scroll the now to do task shows up in the screen.

### Show criteria weight:
Show the calculated weight for each criteria on the result page so that the users can see what  influenced the final score

### User history page:
Add a page where the logged in users can see their history of decisions.
