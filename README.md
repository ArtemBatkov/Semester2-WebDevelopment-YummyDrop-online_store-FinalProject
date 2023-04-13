# CheckIns History of the project
## I will share my progress here and explain what issues I faced while I was working. 
### Check-In #1


<div style="max-width: 800px; word-wrap: break-word;">
In this check-in, I solved some problems. 

  <br> I integrated my injection to the tests. However, I noticed an interesting thing. I am launching the program by console and using the command "dotnet run". 
After that, I can run tests inside of VS. The interesting thing is that if you make GET request to my API, call the method getLastObject1, and then try to compare them
inside the test, it will fail. The reason I explored, is that the program works in one thread and tests are working in others. And it leads to the fact why it isn't possible to reach the breakpoint.

  <br> I kind of solved the issue but calling the GET method directly, instead of using an HTTP request, and it is solved my problem. Now I can compare the expected object and actual ones. However, it is not good. 
  Anyway, I added other tests to be sure that the API returns the object related to YummyItem class, and that each object is unique (5 attempts). 
  
  <br> Next step is starting to explore JavaScript and apply it to the animation of scrolling the box.
</div>




 
