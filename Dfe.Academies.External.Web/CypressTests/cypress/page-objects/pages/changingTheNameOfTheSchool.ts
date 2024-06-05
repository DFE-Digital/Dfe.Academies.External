class ChangingTheNameOfTheSchool {
  public changingTheNameOfTheSchoolElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('h1').contains('Changing the name of the school')
    cy.get('#role-hint').contains('Is the school planning to change its name when it becomes an academy?')
    cy.get('#selectoptionYes').should('not.be.checked')
    cy.get('label[for=selectoptionYes]').contains('Yes')
    cy.get('#selectoptionNo').should('not.be.checked')
    cy.get('label[for=selectoptionNo]').contains('No')
    cy.get('input[type=submit]').should('be.visible').contains('Save and return to overview')
    return this
  }

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
