Feature: register project
	As a manager
	I want to register projects for customers
	So that consultants can register how much time they have worked
	

Scenario: Happy case
	Given customer Finnair exists
	When project Phone app is added for Finnair with activity and consultant
	Then Phone app is ready to get time registrations