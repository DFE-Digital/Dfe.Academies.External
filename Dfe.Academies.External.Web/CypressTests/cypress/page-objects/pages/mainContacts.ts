class MainContacts {
  // TODO get better selectors for elements
  public enterMainContactDetails(headTeacherName: string, headTeacherEmail: string,
    chairName: string, chairEmail: string, approverName: string, approverEmail: string): this {
    cy.get('#ViewModel\\.ContactHeadName').type(headTeacherName)
    cy.get('#ViewModel\\.ContactHeadEmail').type(headTeacherEmail)

    cy.get('#ViewModel\\.ContactChairName').type(chairName)
    cy.get('#ViewModel\\.ContactChairEmail').type(chairEmail)

    cy.get('#ContactTypeChairOfGoverningBody').click()
    cy.get('#ContactTypeChairOfGoverningBody').should('be.checked')

    cy.get('#ApproverContactName').type(approverName)
    cy.get('#ApproverContactEmail').type(approverEmail)
    cy.get('input[type=submit]').click()

    return this
  }
}

const mainContacts = new MainContacts()

export default mainContacts
