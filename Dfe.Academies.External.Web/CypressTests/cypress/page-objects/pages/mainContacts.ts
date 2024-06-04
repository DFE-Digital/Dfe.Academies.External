class MainContacts {
  public mainContactsNotStartedElementsVisible(): this {
    cy.mainContactsNotStartedElementsVisible()

    return this
  }

  public fillMainContactDetailsAndSubmit(): this {
    cy.fillHeadTeacherDetails()

    cy.fillChairDetails()

    cy.get('#ContactTypeChairOfGoverningBody').click()
    cy.get('#ContactTypeChairOfGoverningBody').should('be.checked')

    cy.fillApproverDetails()
    cy.get('input[type=submit]').click()

    return this
  }
}

const mainContacts = new MainContacts()

export default mainContacts
