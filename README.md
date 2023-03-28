The solution contains an asp.net project and unit tests project.<br> Also, for testing purposes, I developed a simple JS SPA that provides a client UI (index.html).<br>
<p><b>BankingSystemTest</b> is an ASP.NET Core 7 REST API and provides endpoints to communicate with Authentication and Account services. <p>The API secure with <b>JWT authentication.</b><p>
<p><b>Authentication controller</b> provides two actions:
<li>api/authentication/signup - creates a new user in the system.<br>
<li>api/authentication/login - returns a JWT token.
</p>
<p><b>Account controller</b> provides following actoin:
<li>api/account/ - returns the list of user's accounts.<br>
<li>api/account/create - creates a new account for the user.<br>
<li>api/account/update - allows to credit or debit the balance.<br>
<li>api/account/delete - deletes an account.<br>
</p>
<p><b>TestProject1</b> contains the unit tests.</p>
