class WhatIsTheNameOfTheSchool {
  public whatIsTheNameOfTheSchoolElementsVisible(): this {
    cy.whatIsTheNameOfTheSchoolElementsVisible()

    return this
  }

  public selectSchoolName(): this {
    cy.selectSchoolName()

    return this
  }
}

const whatIsTheNameOfTheSchool = new WhatIsTheNameOfTheSchool()

export default whatIsTheNameOfTheSchool
