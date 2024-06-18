class WhatIsTheNameOfTheSchool {
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
