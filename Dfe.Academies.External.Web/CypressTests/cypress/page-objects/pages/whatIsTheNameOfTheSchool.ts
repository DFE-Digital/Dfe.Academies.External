class WhatIsTheNameOfTheSchool {
  public selectSchoolName(schoolNameSearchPartial: string): this {
    cy.get('[data-cy="schoolSearchBox"]').click()
    cy.get('[data-cy="schoolSearchBox"]').type(schoolNameSearchPartial)
    // Choose item in the list
    cy.get('#SearchQueryInput__option--0').click()
    cy.get('#ConfirmSelection').click()
    cy.get('#btnAdd').click()

    return this
  }
}

const whatIsTheNameOfTheSchool = new WhatIsTheNameOfTheSchool()

export default whatIsTheNameOfTheSchool
