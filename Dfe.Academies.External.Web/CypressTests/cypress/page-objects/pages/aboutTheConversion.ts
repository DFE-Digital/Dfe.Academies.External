class AboutTheConversion {
  public startContactDetails(): this {
    cy.get('[data-cy="startSectionButton"]').eq(0).click()

    return this
  }

  public checkAboutTheConversion(headTeacherName: string, headTeacherEmail: string,
    chairName: string, chairEmail: string, approverName: string, approverEmail: string): this {
    cy.get('[data-cy="response"]').eq(0).contains(headTeacherName)

    cy.get('[data-cy="response"]').eq(1).contains(headTeacherEmail)

    cy.get('[data-cy="response"]').eq(2).contains(chairName)

    cy.get('[data-cy="response"]').eq(3).contains(chairEmail)

    cy.get('[data-cy="response"]').eq(4).contains('The chair of the governing body')

    cy.get('[data-cy="response"]').eq(5).contains(approverName)

    cy.get('[data-cy="response"]').eq(6).contains(approverEmail)

    cy.get('[data-cy="response"]').eq(7).contains('No')

    cy.get('[data-cy="response"]').eq(8).contains('Reason to join this trust')

    cy.get('[data-cy="response"]').eq(9).contains('No')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const aboutTheConversion = new AboutTheConversion()

export default aboutTheConversion
