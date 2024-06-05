class WhatIsTheNameOfTheSchool {
  public whatIsTheNameOfTheSchoolElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('h2').eq(0).contains('School details')
    cy.get('h2').eq(1).contains('What is the name of the school?')
    cy.get('label').contains('Enter the name of the school, or its 6 digit unique reference number (URN)')
    cy.get('#SearchQueryInput').should('exist')
    cy.get('#btnAdd').should('be.visible').contains('Save and continue')

    return this
  }

  public selectSchoolName(schoolNameSearchPartial: string): this {
    cy.get('.autocomplete__wrapper > #SearchQueryInput').click()
    cy.get('.autocomplete__wrapper > #SearchQueryInput').type(schoolNameSearchPartial)
    cy.get('#SearchQueryInput__option--9').click()
    // TODO Remove reliance on wait
    // eslint-disable-next-line cypress/no-unnecessary-waiting
    cy.wait(2000)
    cy.get('#ConfirmSelection').click()
    cy.get('#btnAdd').click()

    return this
  }
}

const whatIsTheNameOfTheSchool = new WhatIsTheNameOfTheSchool()

export default whatIsTheNameOfTheSchool
