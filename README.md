# DECISION HELPER
A web based decision making tool made using ASP.NET Core MVC that helps to compare options based on weighted criteria.

## MY UNDERSTANDING OF THE PROBLEM
My understanding of the problem was that I had to make a decision making system which gives the best decision to the user from the options the user listed.
Users should be able to define n number of options, their criteria's which affect their decision, how much a criteria affects their decision and also the values of criteria's of each option.
This system solves the problem of user not being able to take a decision because of their emotion.

## ASSUMPTIONS MADE
Users can enter the values of every criteria for each option: Either decimal of Integer and not fixed to a limit to allow flexibility for a real world number such as distance.

Higher is not always better: There can be criteria's where 

## WHY I STRUCTURED THE SOLUTION THIS WAY

The project follows an MVC design pattern so increase readability and scalability

### DecisionController:
    handles all the decision based things such as fetching recommendations.

### AppDbContext:
    is the Entity Framework core of this project which keeps the database.

### DecisionService:
    Implements three steps: ROC(Rank Order Centroid) weight calculation, Pairwise comparison,, Min-Max Normalisation.

### Models:
	  DecisionInput, Option, Criterion, ScoreEntry, ChooseFromTwo:
		  Data for table.

### Flair, DecisionRecord and DecisionOptions:
    are models which record what happened after a decision is made so that it can help with future recommendation to uses.

Frontend is simple server rendered with HTML, vanilla JS and tailwind.

### DecisionServive:

  Calculate function inside this service runs 3 steps,,
    1. ROC Weights: criteria's are ranked in the order of priority(In the order the user added them. User can also rearrange after adding.).
       Rank order centroid weights are calculated using
     		    weight(i) = (1/n) * (1/i + 1/(i+1) + 1/(i+2) + ... + 1/n)
       Then the values of weight(i) is normalised to add up to 1. This gives the top criteria the most weight.
  
  2. Pair wise comparison: For the top 3 criterion, a best of two comparison is made in which the user answers which is more important to them or are they equally important. If a criterion is selected, 12% of the weight        is transferred from the non chosen criteria to the chosen one. Then the weights are normalised again. This helps to adjust the ROC based weight to be adjusted more based on user preferences.

  3. Normalised score: For each criterion, the scores are Mix-Max Normalised in the range 0 to 1. In cases where lower is better, the values are inverted to 1-value. Then the final score become weight * normalised score.


## DESIGN DECISIONS AND TRADE-OFFS

  Flair stored as string with check for duplication. This is simple to implement, allows case insensitive selection thus further preventing duplication.
    But there is no UI for flare management so deletion and editing of flair becomes hard.

  Pairwise comparison of the top 3 criteria only makes the user experience better but can affect the precision.

  User are to enter to value of each criteria to all the option in numbers. Makes the calculation simpler and UX better but some may find it hard to convert a physical feeling to a number. (In cases like taste of a food).

## EGDE CASES CONSIDERED

  Duplicate flair names: Controller searches does a case insensitive search to avoid duplivate flairs

  Adding a flair can bother some people so it is made optional.

  Score input checks if the value is an integer to avoid error in case of non numberical inputs

  Missing option/criteria: If any option or criteria is having no input user gets a notification/The empty once are not considered.

  Pairwise comparison unanswered: Then they get marked as answered and no change in weight happens.

  Recommended Option already added: checks if the recommended option is already added, only adds if not already added

  Multiple options with same winning final score: The first option which was inputed is shown as the result


## HOW TO RUN THE PROJECT

### Prerequisites
  .NET8 SDK
  SQL Server

### Steps:
  1. Clone the repository

	  git clone https://github.com/Yadhu121//DecisionHelper
	  cd DecisionHelper

  2. Configure the database connection in appsettings.jsomn

	  Server=(localdb)\mssqllocaldb;Database=DecisionHelper;Trusted_Connection=True;

  3. Apply database migrations
	
	  dotnet ef database update

  4. Run
	  dotnet run

  5. Open the browser and navigate to the the localhost:<portnumber>


## WHAT I WOULD IMPROVE WITH MORE TIME
    Flair management: Add an admin to manage the flair, rename, edit, delete them,
    Frontend: Switch to a scalable framework.

