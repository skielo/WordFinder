# Word Finder

### Objective: 
The objective of this challenge is not necessarily just to solve the problem - but to evaluate your software development skills, code quality, analysis, creativity, and resourcefulness as a potential future colleague. Please share the necessary artifacts you would provide to your colleagues in a real-world professional setting to best evaluate your work.

Presented with a character matrix and a large stream of words, your task is to create a Class that searches the matrix to look for the words from the word stream. Words may appear horizontally, from left to right, or vertically, from top to bottom. In the example below, the word stream has four words and the matrix contains only three of those words ("chill", "cold" and "wind"):
 
The search code must be implemented as a class with the following interface:

```
public class WordFinder
{ 	
	public WordFinder(IEnumerable<string> matrix) { 
    ... 
    }
    public IEnumerable<string> Find(IEnumerable<string> wordstream) { 
    ... 
    }
}

```

The WordFinder constructor receives a set of strings which represents a character matrix. The matrix size needs to be 64x64, all strings contain the same number of characters.
The "Find" method should return the top 10 most repeated words from the word stream found in the matrix. If no words are found, the "Find" method should return an empty set of strings. If any word in the word stream is found more than once within the stream, the search results should count it only once.

Due to the size of the word stream, the code should be implemented in a **high performance** fashion both in terms of efficient algorithm and utilization of system resources. Where possible, please include your analysis and evaluation.

## Implementation

### Strategy on the WordFinder Class

#### Assumptions

I assumed the **IEnumerable<string> matrix** list that is used to initialize the WordFinder class contains each row of the matrix. I also validate the matrix is neither null or empty. In each case I throw an exception. As part of the requirements I also validate the maxtrix doesn't exceed 64x64.

#### Strategy

The strategy I used for the _Find_ method is the following:

- I iterate over the list of words within the _wordstream_.
- Then for each word I iterate over the board.
- The iteration over the board it's with two for to optimize resources.
    - I verify if the first character of the word to search it's equal to the character in the give possition.
    - As well I cal the method _FindWithDirection_ with the right paramenters, if both conditions are true then I add that word to the list of results.
    - The method _FindWithDirection_ it's responsible to itereate over the board in order to find the rest of the word. It receives the current possition and the count of how many chars of the given word has found already. The method use it as base condition: Once the count it's equals to the length of the word to search it returns true. The method iterates recursively to find  in all directions in case it's the first character of the word. once it picks a direction sticks with it to prevent find the word in a snake like way.

## Running UnitTest

The solution has a UnitTest project in order to quickly validate the different use cases. by executing the unit test on the Class FinderTest.cs you will be able to validate different use cases and validations. 

> The test cases are based on the given exercise. The idea is to validate the minimum vuable product.

To execute the test cases and validate the all of them pass you can right click on the class file and click on _Run Test(s)_

## Running the applications

The solution has two different ways to execute. You can either select the *_console app_* or the *_rest api_* each of them has a different way to execute it.

#### Running the Console app

In order to execute the console app you need to select it first as _startup project_. 

- Right click over the solution name
- Set Statup Projects
- Select the Finder.ConsoleApp project.
- Press F5

A console application is going to prompt. You need to follow the instructions. You are going to be prompted to type the path to a file which is going to serve as grid *matrix*. Each line on that file will be a row
in the matrix. You can have a maximum of 64 lines and each line should have a maximum of 64 characters.

Once you select the file the application console is going to ask you for the words stream. You need to provide another file path but this time should have a single line that contains all the words to search separated with a black space.
> If you provde more than one line the application is going to considere only the first one.

#### Running the Rest API

In order to execute the REST API you need to select it first as _startup project_. 

- Right click over the solution name
- Set Statup Projects
- Select the Finder.REST project.
- Press F5

> Once the application started you need to use an external tool to comunicate with it. I recomend to use [POSTMAN](https://www.postman.com/).

The REST API application has two endpoints:
- *_api/init_*
- *_api/find_*

In the first method you need to provide a json array with the board. Same constrains as the application console. This will initialize the board on the API application.

***If you call the second method before call the init the application it's going to response an 204 http response code***.

The second method you need to post the words stream as a json array. 