class ChangingTheNameOfTheSchool {
  public changingTheNameOfTheSchoolSelectOptionNo(): this {
    cy.get('#selectoptionNo').click()
    cy.get('#selectoptionNo').should('be.checked')

    return this
  }

  public submitChangingTheNameOfTheSchool(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const changingTheNameOfTheSchool = new ChangingTheNameOfTheSchool()

export default changingTheNameOfTheSchool
