# My Motivation
<pre>
Oh, many revelations bright
Are granted by the Spirit's light,
And skill that's born from faults and strife,
And genius, friend to paradox and life,
And chance, the god of all invention,
That sparks creativity's intention.
</pre>


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

---

### Check-In #2
#### What the new features are: 
<div style="max-width: 800px; word-wrap: break-word;">
1. YummyItem was updated. There are many new fields in the YummyItem, one of them is the Image. 
<br>2. GeneratorService and RandomizeService were totally rewritten. Plus, the same realization that is used for lists were applied for array, because it allows a billion times faster to complete UnitTest. 
<br>3. UnitTest also was added. There are many new methods. However the most important is the method that calculates the error of distribution items. It is the slowest and the most time/recources expensive method. 
<br>4. Also, I created shared libraries, and shifted my models and DbContext there. 
<br>5. CartService was added to my Service list. This service allows to manipulate with objects that are inside the cart. Plus it updates the number in the top page navigation. 
<br>6. GeneratorService generates ids by each item drop chance. So, now  actual drop chance matches (including errors) the theoretica drop chance.
<br>7. Because of YummyItem updates, the API Get() also was updated. 
<br>8. IntegrationTests and UnitTest were significant updated. However, there are some integration methods that were not created because of lack of time.
<br>9. UI design was created. Let's consider more close:
<br><br>
Here is a new navigation cell.

![image](https://user-images.githubusercontent.com/110242091/232183315-46f0d206-ef94-40f5-96a7-47a185242109.png)

<br>
Here is the page looks like
  
<br>

![image](https://user-images.githubusercontent.com/110242091/232184152-69345365-2c5c-44c1-b6ac-f6120337ac6a.png)


<br>
There are three buttons. First is production, two others are more for testing in real time.

<br> When I am randoming my item this window appears. Now it can be cancled, but I tried to prevent it, but nothing works.
![image](https://user-images.githubusercontent.com/110242091/232183456-a0de6420-9980-4b9b-839c-a7b4e285b8e8.png)

<br>
Accept accepts the item to the list. Deny closes the window.

<br> You can see that it was added to my cart. And the number was also updated.
![image](https://user-images.githubusercontent.com/110242091/232183486-3c6a6d19-a3fd-4876-b333-95ef76d3bf7a.png)


<br>The cart page looks like so.
![image](https://user-images.githubusercontent.com/110242091/232183522-b5c97965-b6d5-40a4-9d7f-7cd582930a6e.png)
</div>

<br>
#### Problems are:
<div style="max-width: 800px; word-wrap: break-word;">
<br>1. Because of DbContext is a shared library, I can't do a new migration therefore I cant create a new CartTable.
I tried the following:  

  <pre>   
   builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            //options.UseInMemoryDatabase("FruitBoxTable");
        });
        
        ---
        dotnet add-migration CartTable
        ---
        Your target project 'YummyDrop_online_store' doesn't match your migrations assembly 'DbContextSharLab'. Either change your target project or change your migrations assembly.
Change your migrations assembly by using DbContextOptionsBuilder. E.g. options.UseSqlServer(connection, b => b.MigrationsAssembly("YummyDrop_online_store")). By default, the migrations assembly is the assembly containing the DbContext.
Change your target project to the migrations project by using the Package Manager Console's Default project drop-down list, or by executing "dotnet ef" from the directory containing the migrations project.
    </pre>

<br>2.  The Modal window is outside clickable, so a user can escape it. The function that must fire everytime when I close it doesn't work.<br> I tried:
<pre>
 
  &lt;Blazorise.Modal @bind-Visible="showModal" ModalSize="Blazorise.ModalSize.Small" OnClosing="HandleClosing" PreventClosing="true" @ref="modalRef"&gt;
    &lt;div class="modal-dialog modal-dialog-centered modal-dialog-scrollable"&gt;
        &lt;div class="modal-content"&gt;
            &lt;ModalHeader&gt;
                &lt;h3&gt;Modal Title&lt;/h3&gt;
            &lt;/ModalHeader&gt;
            &lt;ModalBody&gt;
                &lt;p&gt;Modal Body Content&lt;/p&gt;
            &lt;/ModalBody&gt;
            &lt;ModalFooter&gt;
                &lt;button class="btn btn-success" @onclick="onAcceptItem"&gt;Take&lt;/button&gt;
                &lt;button class="btn btn-danger" @onclick="onSellItem"&gt;Sell&lt;/button&gt;
            &lt;/ModalFooter&gt;
        &lt;/div&gt;
    &lt;/div&gt;
&lt;/Blazorise.Modal&gt;

            
            --- 
             Blazorise.Modal modalRef;

    private void HandleClosing(Blazorise.ModalClosingEventArgs e)
    {
        Console.WriteLine("HandleClosing method called");
        e.Cancel = true;
    }
The method is not fired!
</pre>
</div>
