class WhatIsTheNameOfTheSchool {
  // TODO get better selectors
  public selectSchoolName(schoolNameSearchPartial: string): this {
    cy.get('.autocomplete__wrapper > #SearchQueryInput').click()
    cy.get('.autocomplete__wrapper > #SearchQueryInput').type(schoolNameSearchPartial)
    cy.get('#SearchQueryInput__option--9').click()
    cy.get('#ConfirmSelection').click()
    cy.get('#btnAdd').click()

    return this
  }
}

const whatIsTheNameOfTheSchool = new WhatIsTheNameOfTheSchool()

export default whatIsTheNameOfTheSchool
