class LandAndBuildingsSummary {
  public landAndBuildingsSummaryElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Land and buildings')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

    cy.get('b').eq(0).contains('As far as you\'re aware, who owns or holds the school\'s buildings and land?')
    cy.get('p').eq(2).contains('You have not added any information')

    cy.get('b').eq(1).contains('Are there any current or planned building works?')
    cy.get('p').eq(4).contains('You have not added any information')

    cy.get('b').eq(2).contains('Are there any shared facilities on site?')
    cy.get('p').eq(6).contains('You have not added any information')

    cy.get('b').eq(3).contains('Has the school had any grants from Sport England, the Big Lottery Fund, or the Football Federation?')
    cy.get('p').eq(8).contains('You have not added any information')

    cy.get('b').eq(4).contains('Is the school part of a Private Finance Initiative (PFI) scheme?')
    cy.get('p').eq(10).contains('You have not added any information')

    cy.get('b').eq(5).contains('Is the school part of the Priority School Building Programme?')
    cy.get('p').eq(12).contains('You have not added any information')

    cy.get('b').eq(6).contains('Is the school part of the Building Schools for the Future Programme?')
    cy.get('p').eq(14).contains('You have not added any information')

    return this
  }

  public selectLandAndBuildingsStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public landAndBuildingsSummaryCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Land and buildings')

    cy.get('.govuk-link').contains('Change your answers')

    cy.get('b').eq(0).contains('As far as you\'re aware, who owns or holds the school\'s buildings and land?')
    cy.get('p').eq(2).contains('As far as you\'re aware, who owns or holds the school\'s buildings and land?')

    cy.get('b').eq(1).contains('Are there any current or planned building works?')
    cy.get('p').eq(4).contains('No')

    cy.get('b').eq(2).contains('Are there any shared facilities on site?')
    cy.get('p').eq(6).contains('No')

    cy.get('b').eq(3).contains('Has the school had any grants from Sport England, the Big Lottery Fund, or the Football Federation?')
    cy.get('p').eq(8).contains('No')

    cy.get('b').eq(4).contains('Is the school part of a Private Finance Initiative (PFI) scheme?')
    cy.get('p').eq(10).contains('No')

    cy.get('b').eq(5).contains('Is the school part of the Priority School Building Programme?')
    cy.get('p').eq(12).contains('No')

    cy.get('b').eq(6).contains('Is the school part of the Building Schools for the Future Programme?')
    cy.get('p').eq(14).contains('No')

    return this
  }

  public submitLandAndBuildingsSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const landAndBuildingsSummary = new LandAndBuildingsSummary()

export default landAndBuildingsSummary
