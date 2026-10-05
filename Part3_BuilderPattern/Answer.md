### Task 3.1 
#### Q1
single 20 parameters constructor is not readable anymore and we should know the arrangement of each property to not send anyone of them in a wrong place, and this takes us to the risk of passing values in wrong order , If the compiler detects it its a good news but what if we pass values with the same types in wrong orders ? the compile will not detect it and we will get an non logical result at the end , the problem not stopping at that point. what will happen if we want to add some new attributes to this class ? the case will be worst for sure .
#### Q2
of course there is a deeper design issue with putting 20 loosely related properties with each other in one single class . the class has unrelated responsibilities like customer data , calculations , shipping and payment info . we should put related fields with each other in a separate classes and responsibilities to apply one of the most important principles of SOLID which is single responsibility.


### Task 3.3
•  what does each small builder own, and only own?
	 - After applying the single responsibility principle each class own data related to each other :-
		 1- Invoice class containing customer information  and address of shipping and billing as      references.
		 2- Address class containing details about the street , city , state , zip code, country and so .
		 3- OrderPayment class contains data about the payment method and  tax and subtotals    with automatically calculated total  
	   so each separated class own data and behaviors related to each other.
	   
•  can AddressBuilder guarantee a complete address on its own, without the parent object knowing anything about street/city/zip rules? 
	    - Yes that is what we have done at the end the AddressBuilder only the class who know about street city , country and state the parent class only checks that it doesn't receive a null Address


• the exact same AddressBuilder is used for both billing and shipping. What would you have had
to duplicate without it?
		-   without it i would had to build the shippingBuilder class and BillingBuilder class which all of them has the same properties 


• Readability at the call site — compare constructing the object with Task 3.2's single builder versus this composed version? 
		-  the address and order arguments are grouped inside named builders (billing, shipping, order) so you can't mix up city and country, and the final InvoiceBuilder call shrinks from 9 arguments to 5.  
		- 
The single builder is only shorter for simple cases, since it's one chain with no extra variables, but its long list of positional strings is easy to get wrong.