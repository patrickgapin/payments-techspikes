function getStatuses()
	{
	    // Statuses are hard-coded for the sake of this example.
	    // In a real app you would probably use xmHttpRequest (Ajax)
	    // to load that data on the client and return from this function
	    var statuses =
		[
			{ ID: 1, Name: "Success" },
			{ ID: 2, Name: "Warning" },
			{ ID: 3, Name: "Error" }
		];
 
	    return statuses;
	};
 
