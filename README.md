Desktop auction application created with WPF, C# and Microsoft SQL Server 2014.

Note: I've used .NET Framework 4.8.1, Microsoft Visual Studio 2022 lost its Long-Term Support for it, so it won't install automatically. To open the app, .NET Framework 4.8.1 must be installed manually.

This app has 2 users: administrator and regular user.

-Administrator can add new products and remove products. Adding products will start the auction, and a timer of 2 minutes will start ticking.

-Regular user can offer the highest price; when he clicks on the "offer" button, the price increases by 1 euro and the timer restarts.

When the 2-minute timer has expired, the auction ends.

I didnt't make a sign-up page; the users must be added directly in the database.

Patterns used:

-MVVM.

-Mediator.

-Singleton.
