// TODO pull out methods from commands.ts

class TrustOverview {
  public FAMTrustOverviewTrustNameCompleteElementsVisible(): this {
    cy.FAMTrustOverviewTrustNameCompleteElementsVisible()

    return this
  }

  public selectOpeningDate(): this {
    cy.contains('Opening date').click()

    return this
  }

  public FAMTrustOverviewOpeningDateCompleteElementsVisible(): this {
    cy.FAMTrustOverviewOpeningDateCompleteElementsVisible()

    return this
  }

  public selectReasonsForFormingTheTrust(): this {
    cy.contains('Reasons for forming the trust').click()

    return this
  }

  public FAMTrustOverviewReasonsForFormingTrustCompleteElementsVisible(): this {
    cy.FAMTrustOverviewReasonsForFormingTrustCompleteElementsVisible()

    return this
  }

  public selectPlansForGrowth(): this {
    cy.contains('Plans for growth').click()

    return this
  }

  public FAMTrustOverviewPlansForGrowthCompleteElementsVisible(): this {
    cy.FAMTrustOverviewPlansForGrowthCompleteElementsVisible()

    return this
  }

  public selectSchoolImprovementStrategy(): this {
    cy.contains('School improvement strategy').click()

    return this
  }

  public FAMTrustOverviewSchoolImprovementStrategyCompleteElementsVisible(): this {
    cy.FAMTrustOverviewSchoolImprovementStrategyCompleteElementsVisible()

    return this
  }

  public selectGovernanceStructure(): this {
    cy.contains('Governance structure').click()

    return this
  }

  public FAMTrustOverviewGovernanceStructureCompleteElementsVisible(): this {
    cy.FAMTrustOverviewGovernanceStructureCompleteElementsVisible()

    return this
  }

  public selectKeyPeople(): this {
    cy.contains('Key people').click()

    return this
  }

  public FAMTrustOverviewKeyPeopleCompleteElementsVisible(): this {
    cy.FAMTrustOverviewKeyPeopleCompleteElementsVisible()

    return this
  }

  public selectReturnToYourApplication(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const trustOverview = new TrustOverview()

export default trustOverview
