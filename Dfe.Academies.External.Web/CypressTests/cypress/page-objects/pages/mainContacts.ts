class MainContacts {
  public enterMainContactDetails(headTeacherName: string, headTeacherEmail: string,
    chairName: string, chairEmail: string, approverName: string, approverEmail: string): this {
    cy.get('[data-cy="contactHeadName"]').type(headTeacherName)
    cy.get('[data-cy="contactHeadEmail"]').type(headTeacherEmail)

    cy.get('[data-cy="contactChairName"]').type(chairName)
    cy.get('[data-cy="contactChairEmail"]').type(chairEmail)

    cy.get('#ContactTypeOther').click()
    cy.get('#ContactTypeOther').should('be.checked')

    cy.get('#ApproverContactName').type(approverName)
    cy.get('#ApproverContactEmail').type(approverEmail)
    cy.get('input[type=submit]').click()

    return this
  }
}

const mainContacts = new MainContacts()

export default mainContacts
