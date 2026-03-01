\## HOW I STARTED
 
The first thing I did was to see if there was any existing decision companion system to see what was already there.
 
What I got was sites like
 
[https://decideformenow.com/](https://decideformenow.com/) 
 

which just selected a random decision
 
and
 
[https://www.yeschat.ai/gpts-2OTocH6A7x-Decision-AI](https://www.yeschat.ai/gpts-2OTocH6A7x-Decision-AI) 
 

which completely depended on an AI to make a decision
 
Both of these type I could not refer, upon further searching I found
 
[https://decisionchamp.com/](https://decisionchamp.com/) 
 
Which satisfied the requirements I was looking for so I decided to use that as a base.
 
So, at first, in my mind I was going to build a fully weight based system which would select the best decision.
 
\## HOW MY THINKING EVOLVED
 
\### 1:
 
I started with a CLI using python which was like the mentioned website and used the same logic, that is, asks the user for option, asks the user for criteria, asks the user to weight the criteria and asks the user to rank the options based on criteria.
 
I used weighted average for calulation
 
val1  *​w1​ + val2*  ​w2​+ ... +valn \* ​wn​
 
Calculated average weight for each option and then compared and gave the option with higher average as the decision
 
This works at first, but upon testing I faced some problems like, There were come criteria's which cannot be grouped into same weight category, for example a factors such as Taste can be ranked out of 10, but I found it hard to reduce criteria's such as price to be in the same scale when each options have different prices. Also giving price with other factors which were in different weight classification would result in very non accurate result.
 
\### 2:
 
So I decided to covert all the types into a same scale using normalisation.
 
(val1  *w1 + val2*  w2 + ... + valn \* wn)/(w1+w2+...+wn)
 
This resulted in different criteria's of different scale to be evaluated properly, solving the issue.
 
\### 3:
 
Then I started converting it into a webapp
 
\### 4:
 
The next unclear thing in my mind while in that phase was how can someone say how much weight is for criteria, for example performance?. So I wanted to change the way of giving weights to criteria's.
 
So I looked into AHP(Analytic hierarchy process) in which all the criteria's are compared with each other to find their weighting.
 
But the problem I faced with this approach was when there are many criteria's. It was fine if there are only 3-4 criteria's, when the number of criteria's increases the amount of options the user should select also increased the number gets significantly bigger with increase in number of ciriteria
 
total selections needed = (n(n-1))/2
 
So as another approach to solve this problem I decided to instead of giving weights to criteria or doing full pair wise comparison, order the criteria in the order of priority and give them weights based on ROC(Rank order centroid)
 
weight(i) = (1/n) × (1/i + 1/(i+1) + 1/(i+2) + ... + 1/n)
 
So now the criterias are ranked with weights based on number of criteria's decreasing through the list.
 
\### 5:
 
Another problem with this I faced is the gap, ie the user may prefer one criteria way more than the next so I compare the top 3 criteria's with each other and pass 12% of the weight from the non selected criteria to the selected one with it is selected as equally important no weight transfer occurs.
 
\## ALTERNATIVE APPROACHES CONSIDERED
 
\### 1. Weights directly assigned by user
 
Users gave the weights to the criteria's directly: Didn't use this approach as users might find it hard to assign a numerical value to physical factors and also might affect UX
 
\### 2. Fully use Analytic Hierarchy Process
 
Ask the users which criteria matters more to them comparing all the criteria's with each [other.It](http://other.It)  would require many comparisons and would significantly affect the user experience
 
\### 3. API call to an LLM
 
Passes the options and the criteria to an LLM using an API and let it decide. Not used because the output fully depended on an AI.
 
\### 4. Input as sliders or star rating instead of numbers
 
Let users rate each option per criterion using a slider instead of typing a number. Not used because these removes the ability to enter real values like price or distance.
 
\## REFACTORING DECISIONS
 
Calculation logic was moved from Controller to a new DecisionService to improve readability and to keep the code according to MVC principles
 
Input models (DecisionInput, Options, Criterion, ScoreEntry, ChooseFromTwo) were kept separate from other models (Flair, DecisionRecord, DecisionOption, UserDecisionOption, UserCriterion). This keeps data distinct from the data used for storage and recommendations.
 
The Min Max normalisation logic was kept inside DecisionService to prevent duplication.
 
Renumbering logic: When an option or a criterion is reordered or removed the renumberingof all the remaining items is done by dedicated functions.
 
\## MISTAKES AND CORRECTIONS
 
1\. At first the calculations didn't add up to 1 since I didn't use normalisation and the results were very inaccurate
 
2\. Lower better criteria was added later, so at first everything was considered as higher is better, so in cases of criteria's such as price it affected negatively.
 
3\. All the weights were first taken from the user over complicating it for the user and negatively affecting the UX
 
4\. The options and criteria went forward even if there were nothing inserted into it, this causes accidental insertion of non existent options and criteria's
 
5\. Flair duplicated were being created when a user tries to create a flair which already exists.
 
6\. The recommended options selected from the flair could be added more than 1 time at first.
 
7\. The score would not reset when a option or crteria was changed.
 
\## WHAT CHANGED DURING DEVELOPMENT AND WHY
 
\### Addition of Pairwise Comparison
 
Originally, only ROC weights were used.
 
ROC alone assigns weights based on order but cannot capture how much more a user values one criterion than others. So a pair wise comparison was added for the top 3 criteria which would allow to fine tune the weights while not affecting the UX
 
\### Flairs and storage of decision history
 
The initial version only created result and didn't store any of it. Flairs were added to group decisions by topic. This made it possible to store options and give them as recommendations to users in a meaningful way.
 
Logged in users also get personalised recommendations based on flairs. They get recommended their previous options and criteria's in that flair while guest users gets suggestions of most used flairs in that flair.
 
\### Higher/Lower toggle added to criteria
 
originally all the criteria were like higher is better. Changed it so that user can select whether lower/higher is better of each criterion specifically
 
\### Flair made optional
 
Flair was first required then it was made optional which would help users when they want a fast decision and don't want to do any extra steps