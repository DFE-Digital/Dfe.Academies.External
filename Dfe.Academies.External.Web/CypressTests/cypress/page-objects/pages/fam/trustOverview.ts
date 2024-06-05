class TrustOverview {
  public FAMTrustOverviewTrustNameCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Form a new multi-academy trust')

    cy.get('.govuk-heading-l').contains('Plymouth')

    cy.get('.govuk-body').contains('Complete all sections.')

    cy.get('strong').eq(1).contains('Completed')
    cy.get('strong').eq(2).contains('Not Started')
    cy.get('strong').eq(3).contains('Not Started')
    cy.get('strong').eq(4).contains('Not Started')
    cy.get('strong').eq(5).contains('Not Started')
    cy.get('strong').eq(6).contains('Not Started')
    cy.get('strong').eq(7).contains('Not Started')

    cy.get('.govuk-button').should('be.visible').contains('Return to your application')

    return this
  }

  public selectOpeningDate(): this {
    cy.contains('Opening date').click()

    return this
  }

  public FAMTrustOverviewOpeningDateCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Form a new multi-academy trust')

    cy.get('.govuk-heading-l').contains('Plymouth')

    cy.get('.govuk-body').contains('Complete all sections.')

    cy.get('strong').eq(1).contains('Completed')
    cy.get('strong').eq(2).contains('Completed')
    cy.get('strong').eq(3).contains('Not Started')
    cy.get('strong').eq(4).contains('Not Started')
    cy.get('strong').eq(5).contains('Not Started')
    cy.get('strong').eq(6).contains('Not Started')
    cy.get('strong').eq(7).contains('Not Started')

    cy.get('.govuk-button').should('be.visible').contains('Return to your application')

    return this
  }

  public selectReasonsForFormingTheTrust(): this {
    cy.contains('Reasons for forming the trust').click()

    return this
  }

  public FAMTrustOverviewReasonsForFormingTrustCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Form a new multi-academy trust')

    cy.get('.govuk-heading-l').contains('Plymouth')

    cy.get('.govuk-body').contains('Complete all sections.')

    cy.get('strong').eq(1).contains('Completed')
    cy.get('strong').eq(2).contains('Completed')
    cy.get('strong').eq(3).contains('Completed')
    cy.get('strong').eq(4).contains('Not Started')
    cy.get('strong').eq(5).contains('Not Started')
    cy.get('strong').eq(6).contains('Not Started')
    cy.get('strong').eq(7).contains('Not Started')

    cy.get('.govuk-button').should('be.visible').contains('Return to your application')

    return this
  }

  public selectPlansForGrowth(): this {
    cy.contains('Plans for growth').click()

    return this
  }

  public FAMTrustOverviewPlansForGrowthCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Form a new multi-academy trust')

    cy.get('.govuk-heading-l').contains('Plymouth')

    cy.get('.govuk-body').contains('Complete all sections.')

    cy.get('strong').eq(1).contains('Completed')
    cy.get('strong').eq(2).contains('Completed')
    cy.get('strong').eq(3).contains('Completed')
    cy.get('strong').eq(4).contains('Completed')
    cy.get('strong').eq(5).contains('Not Started')
    cy.get('strong').eq(6).contains('Not Started')
    cy.get('strong').eq(7).contains('Not Started')

    cy.get('.govuk-button').should('be.visible').contains('Return to your application')

    return this
  }

  public selectSchoolImprovementStrategy(): this {
    cy.contains('School improvement strategy').click()

    return this
  }

  public FAMTrustOverviewSchoolImprovementStrategyCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Form a new multi-academy trust')

    cy.get('.govuk-heading-l').contains('Plymouth')

    cy.get('.govuk-body').contains('Complete all sections.')

    cy.get('strong').eq(1).contains('Completed')
    cy.get('strong').eq(2).contains('Completed')
    cy.get('strong').eq(3).contains('Completed')
    cy.get('strong').eq(4).contains('Completed')
    cy.get('strong').eq(5).contains('Completed')
    cy.get('strong').eq(6).contains('Not Started')
    cy.get('strong').eq(7).contains('Not Started')

    cy.get('.govuk-button').should('be.visible').contains('Return to your application')

    return this
  }

  public selectGovernanceStructure(): this {
    cy.contains('Governance structure').click()

    return this
  }

  public FAMTrustOverviewGovernanceStructureCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Form a new multi-academy trust')

    cy.get('.govuk-heading-l').contains('Plymouth')

    cy.get('.govuk-body').contains('Complete all sections.')

    cy.get('strong').eq(1).contains('Completed')
    cy.get('strong').eq(2).contains('Completed')
    cy.get('strong').eq(3).contains('Completed')
    cy.get('strong').eq(4).contains('Completed')
    cy.get('strong').eq(5).contains('Completed')
    cy.get('strong').eq(6).contains('Completed')
    cy.get('strong').eq(7).contains('Not Started')

    cy.get('.govuk-button').should('be.visible').contains('Return to your application')

    return this
  }

  public selectKeyPeople(): this {
    cy.contains('Key people').click()

    return this
  }

  public FAMTrustOverviewKeyPeopleCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Form a new multi-academy trust')

    cy.get('.govuk-heading-l').contains('Plymouth')

    cy.get('.govuk-body').contains('Complete all sections.')

    cy.get('strong').eq(1).contains('Completed')
    cy.get('strong').eq(2).contains('Completed')
    cy.get('strong').eq(3).contains('Completed')
    cy.get('strong').eq(4).contains('Completed')
    cy.get('strong').eq(5).contains('Completed')
    cy.get('strong').eq(6).contains('Completed')
    cy.get('strong').eq(7).contains('Completed')

    cy.get('.govuk-button').should('be.visible').contains('Return to your application')

    return this
  }

  public selectReturnToYourApplication(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const trustOverview = new TrustOverview()

export default trustOverview
