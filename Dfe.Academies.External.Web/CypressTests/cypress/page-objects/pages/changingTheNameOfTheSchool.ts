class ChangingTheNameOfTheSchool {
  public enterChangingTheNameOfTheSchool(choice: string): this {
    const selector = `#selectoption${choice === 'Yes' ? 'Yes' : 'No'}`

    cy.get(selector).click()
    cy.get(selector).should('be.checked')

    if (choice === 'Yes') {
      cy.get('[id="ChangeSchoolName"').type('New Name')
    }

    cy.get('input[type=submit]').click()

    return this
  }
}

const changingTheNameOfTheSchool = new ChangingTheNameOfTheSchool()

export default changingTheNameOfTheSchool
