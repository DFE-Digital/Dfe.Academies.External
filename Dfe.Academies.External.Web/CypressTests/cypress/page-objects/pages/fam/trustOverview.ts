class TrustOverview {
  // TODO refactor methods down
  // TODO get better selector for element
  public checkTrustNameCompleted(): this {
    cy.get('strong').eq(1).contains('Completed')

    return this
  }

  // TODO get better selector for element
  public selectOpeningDate(): this {
    cy.contains('Opening date').click()

    return this
  }

  public checkOpeningDateCompleted(): this {
    cy.get('strong').eq(2).contains('Completed')

    return this
  }

  // TODO get better selector for element
  public selectReasonsForFormingTheTrust(): this {
    cy.contains('Reasons for forming the trust').click()

    return this
  }

  public checkReasonsForFormingTrustCompleted(): this {
    cy.get('strong').eq(3).contains('Completed')

    return this
  }

  // TODO get better selector for element
  public selectPlansForGrowth(): this {
    cy.contains('Plans for growth').click()

    return this
  }

  public checkPlansForGrowthCompleted(): this {
    cy.get('strong').eq(4).contains('Completed')

    return this
  }

  // TODO get better selector for element
  public selectSchoolImprovementStrategy(): this {
    cy.contains('School improvement strategy').click()

    return this
  }

  public checkSchoolImprovementStrategyCompleted(): this {
    cy.get('strong').eq(5).contains('Completed')

    return this
  }

  // TODO get better selector for element
  public selectGovernanceStructure(): this {
    cy.contains('Governance structure').click()

    return this
  }

  public checkGovernanceStructureCompleted(): this {
    cy.get('strong').eq(6).contains('Completed')

    return this
  }

  // TODO get better selector for element
  public selectKeyPeople(): this {
    cy.contains('Key people').click()

    return this
  }

  public checkKeyPeopleCompleted(): this {
    cy.get('strong').eq(7).contains('Completed')

    return this
  }

  // TODO get better selector for element
  public selectReturnToYourApplication(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const trustOverview = new TrustOverview()

export default trustOverview
