class SuccessfulApplicationSubmitted {
  public checkApplicationSubmitted(): this {
    cy.url().then((url) => {
      const applicationId = url.split('=').pop()

      cy.get('[data-cy="applicationSubmittedTitle"]').contains('Your application has been submitted')

      cy.get('[data-cy="applicationReferenceInformation"]').contains('Your reference number is')

      cy.get('[data-cy="applicationReferenceNumber"]').contains(`A2B_${applicationId}`)
    })

    return this
  }
}

const successfulApplicationSubmitted = new SuccessfulApplicationSubmitted()

export default successfulApplicationSubmitted
