Feature: register time
	As a consultant
	I want to register time on my projects
	So that the customers are being invoiced correctly
	
Scenario: Happy case
	Given consultant Bruce Wayne exists
		And project Cloudy Sky exists and is ready for registration
		And today has 0 hours
	When Bruce Wayne registers 2 hours of programming on project Cloudy Sky for today
	Then today has 2 hours