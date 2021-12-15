﻿Feature: F_DeactivateAccount

Scenario: 1 I want to deactivate an existing Account
	Given There is an active account existing
	When I deactivate the account with reason Test
	Then the account is inactive

Scenario: 2 I want to activate an existing inactive account
	Given There is an inactive account existing
	When I activate the account
	Then The account is active