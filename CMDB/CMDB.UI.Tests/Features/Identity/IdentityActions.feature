﻿Feature: IdentityActions

Scenario: 1 I want to deactivate an existing Identity
	Given An acive Identity exisist in the system
	When I want to deactivete the identity whith the reason Test
	Then The Idenetity is inactive

Scenario: 2 I want to activate an inactive Identity
	Given An inactive Identity exisist in the system
	When I want to activate this identity
	Then The Identity is active