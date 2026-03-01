## AI PROMPTS USED
 
How to do weighted calculation
 
How to find weighted calculation if there are multiple criteria
 
How to find weighted average
 
How to get more accurate weighted average value?
 
How can i improve weighted calculation now I just find the weighted average
 
what is min max normalisation
 
when to use min max normalisation
 
explain the formula of min max normal
 
how to normalise weighted average
 
Should weights always sum to 1
 
What is TOPSIS
 
How can someone perfectly give the weight of a preference
 
what is Analytical Heriarchy process
 
But what if the number of criteria is more then the number of times the user should select increases right Which would affect the UX
 
can I rank it according to order
 
what is ROC
 
but what if the number of criterias is very high
 
difference between AHP and ROC
 
advantages of rank based weighting
 
ROC formula explain
 
How to normalise data to 0-1
 
How to implement ROC in a weighted ranking system>?
 
How to implement ROC in C#
 
should I normalise the roc weight afterwards?
 
how to ensure weights sum to 1
 
Best MVC design practices
 
what if I compare top 3 criteria with each other after ROC
 
How to only the first 3 items of the list
 
should I use a fixed percentage for change?
 
but what if the higher values is not good and lower is better
 
How to normalise values between 0-1 in C#
 
Should I normalize weights if it was modified
 
How to implement min max normalisation in C#
 
how to show ui elements dynamically
 
js dom for changing number with change in order
 
mvc naming conventions
 
Naming conventions for models
 
How does .select() work in c#
 
How to assign computed weights back to objects in a list using linq
 
how to convert linq result back to array
 
how to use .where() in linq
 
what if we add something like flair in reddit which stores the options which was uses while that flair was selected so that when another user select that flair again that user gets suggestions for options
 
only the top 5 most used of that flair should be shown as recommended
 
EF functions
 
How to suggest the options which was most used
 
how to get the best option
 
how to prevent crash when invalid input is entered
 
How to show element of ui only when a condition is satisfied
 
how to hide components when count less than 2
 
How to make input to numbers only
 
How to make UI responsive
 
tailwind utility classes
 
How to centre element using tailwind
 
How to disable submit button until valid
 
minimal looking fonts
 
Should I add a login feature so that we can give logged in users custom personalised recommendations
 
How to migrate form sqlserver to postgresql
 
## GOOGLE SEARCHES
 
Decision making system
 
Weighted average geeks for geeks
 
Ranked order centroid
 
ROC weight formula
 
Tailwind css cdn
 
tailwind css classes
 
google fonts
 
npgsql postgreSQL .net
 
topsis geeksforgeeks
 
## REFERENCES THAT INFLUENCES THE APPROACH
 
### [decisionchamp.com](http://decisionchamp.com) 
 
This website which I found during the initial search influenced my approach. Though I didn't fully make something like this, this is what helped me start. I used it as an overall reference and modified in a way I think is better.
 
### Reddit flair system:
 
The concept of flairs while adding posts in reddit gave me the idea to add flairs in this which would help to track what the decision is about and hence help to give recommendations.
 
### Analytic hierarchy process:
 
First learnt about this to use this as a full weighting method for the entire project but finally decided to use it for pair wise comparison of the top 3 criterion as it makes it hard with increase in number of options.
 
### Rank order centroid:
 
Used this as the primary weight system after looking into order based weighting process.
 
### [tailwind.build](http://tailwind.build) :
 
Usesd this to refer tailwind classes
 
###[railway.com](http://railway.com) :
 
Used to host the website
 
## What Was Accepted, Rejected, or Modified from AI Outputs
 
### Accepted:
 
#### ROC weight formula:
 
The formula and for calculating ROC weights were take and implemented directly after manual verification.
 
#### Min max normalisation formula:
 
The main formula and the formula for lower is better criterions were accepted from AI.
 
#### LINQ methods:
 
LINQ patterns for queries were used.
 
#### EF core:
 
AI also helped with EF core patterns
 
### Rejected:
 
#### TOPSIS:
 
This websites decides on user priorities. The method I used makes it easier to understand why we got the final result.
 
#### Full AHP:
 
Full AHP would lead to very high number of matching to be done when the number of criteria is increased.
 
#### Weights directly assigned by the user:
 
This is not done considering the fact that user may not be able to convert their feeling to preference of one criteria over another into exactly a number.
 
#### Slider based approach:
 
This was also rejected as it would bring the need to add a limit which mean that some real world things like price should also fit inside that limit.
 
#### Showing all the past options as recommendations:
 
This was suggested but was rejected as I though this would be irritating or even hard to find an option from it even if they wish to.
 
### Modified:
 
#### Pairwise comparison:
 
The full AHP suggested my AI was modified to just do the top3
 
##### Flair changed to optional:
 
The flair was suggested to be set as required instead made it optional to let the user choose
