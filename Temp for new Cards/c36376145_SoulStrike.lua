--魂の一撃
function c36376145.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_ATKCHANGE)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetCode(EVENT_ATTACK_ANNOUNCE)
	e1:SetCondition(c36376145.condition)
	e1:SetCost(c36376145.cost)
	e1:SetTarget(c36376145.target)
	e1:SetOperation(c36376145.activate)
	c:RegisterEffect(e1)
end
function c36376145.condition(e,tp,eg,ep,ev,re,r,rp)
	local a=Duel.GetAttacker()
	local at=Duel.GetAttackTarget()
	return Duel.GetLP(tp)<=4000
	and a:IsControler(1-tp) and at
end
function c36376145.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	local lp=Duel.GetLP(tp)/2
	if chk==0 then return Duel.CheckLPCost(tp,lp) and Duel.GetFlagEffect(tp,36376145)==0 end
	Duel.PayLPCost(tp,lp)
	Duel.RegisterFlagEffect(tp,36376145,RESET_PHASE+PHASE_END,EFFECT_FLAG_OATH,1)
end
function c36376145.target(e,tp,eg,ep,ev,re,r,rp,chk)
	if chkc then return chkc:IsLocation(LOCATION_MZONE) and chkc:IsFaceup() and chkc:IsControler(tp) end
	if chk==0 then return Duel.IsExistingTarget(Card.IsFaceup,tp,LOCATION_MZONE,0,1,nil) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_FACEUP)
	local g=Duel.SelectTarget(tp,Card.IsFaceup,tp,LOCATION_MZONE,0,1,1,nil)
end
function c36376145.activate(e,tp,eg,ep,ev,re,r,rp)
	local tc=Duel.GetFirstTarget()
	if tc:IsRelateToEffect(e) and tc:IsFaceup() then
		local e1=Effect.CreateEffect(e:GetHandler())
		e1:SetType(EFFECT_TYPE_SINGLE)
		e1:SetCode(EFFECT_UPDATE_ATTACK)
		e1:SetReset(RESET_EVENT+0x1fe0000+RESET_PHASE+PHASE_END)
		e1:SetValue(4000-Duel.GetLP(tp))
		tc:RegisterEffect(e1)
	end
end
