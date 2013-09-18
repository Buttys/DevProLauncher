--ハーフ・アンブレイク
function c981540.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetTarget(c981540.target)
	e1:SetOperation(c981540.activate)
	c:RegisterEffect(e1)
end
function c981540.target(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chkc then return chkc:IsLocation(LOCATION_MZONE) and chkc:IsFaceup() end
	if chk==0 then return Duel.IsExistingTarget(Card.IsFaceup,tp,LOCATION_MZONE,LOCATION_MZONE,1,nil) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_FACEUP)
	Duel.SelectTarget(tp,Card.IsFaceup,tp,LOCATION_MZONE,LOCATION_MZONE,1,1,nil)
end
function c981540.activate(e,tp,eg,ep,ev,re,r,rp)
	local tc=Duel.GetFirstTarget()
	if tc:IsRelateToEffect(e) and tc:IsFaceup() then
		local c=e:GetHandler()
		local e1=Effect.CreateEffect(c)
		e1:SetType(EFFECT_TYPE_SINGLE)
		e1:SetCode(EFFECT_INDESTRUCTABLE_BATTLE)
		e1:SetReset(RESET_EVENT+0x1fe0000+RESET_PHASE+RESET_END)
		e1:SetValue(1)
		tc:RegisterEffect(e1)
		local e2=Effect.CreateEffect(c)
		e2:SetRange(LOCATION_MZONE)
		e2:SetType(EFFECT_TYPE_FIELD)
		e2:SetCode(EFFECT_CHANGE_DAMAGE)
		e2:SetProperty(EFFECT_FLAG_PLAYER_TARGET)
		e2:SetReset(RESET_EVENT+0x1fe0000+RESET_PHASE+RESET_END)
		e2:SetTargetRange(1,1)
		e2:SetValue(c981540.val)
		tc:RegisterEffect(e2)
	end
end
function c981540.val(e,re,dam,r,rp,rc)
	local c=e:GetHandler()
	if (rc and (rc==c or rc:GetBattleTarget()==c)) then
		return dam/2
	else return dam end
end